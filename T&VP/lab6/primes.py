'''
This module implements simple prime generation and primality testing.
'''

from random import SystemRandom
random = SystemRandom()
from os import urandom

from hashlib import sha1
from math import log

def exp(x, n, m):
	'''Efficiently compute x ** n mod m'''
	y = 1
	z = x
	while n > 0:
		if n & 1:
			y = (y * z) % m
		z = (z * z) % m
		n //= 2
	return y


# Miller-Rabin-Test

def prime(n, k):
	'''Checks whether n is probably prime (with probability 1 - 4**(-k)'''
	
	if n % 2 == 0:
		return False
	
	d = n - 1
	s = 0
	
	while d % 2 == 0:
		s += 1
		d //= 2
		
	for i in range(k):
		
		a = 2 + random.randint(0, n - 4)
		x = exp(a, d, n)
		if (x == 1) or (x == n - 1):
			continue
		
		for r in range(1, s):
			x = (x * x) % n
			
			if x == 1:
				return False
			
			if x == n - 1:
				break
			
		else:
			return False
	return True


# Generate and Test Algorithms

def get_prime(size, accuracy):
	'''Generate a pseudorandom prime number with the specified size (bytes).'''
	
	while 1:

		# read some random data from the operating system
		rstr = list(urandom(size - 1))
		r = 128 | ord(urandom(1))   # MSB = 1 (not less than size)
		for c in rstr:
			r = (r << 8) | c
		r |= 1					  # LSB = 1 (odd)

		# test whether this results in a prime number
		if prime(r, accuracy):
			return r


def get_prime_upto(n, accuracy):
	'''Find largest prime less than n'''
	n |= 1
	while n > 0:
		n -= 2
		if prime(n, accuracy):
			return n

def generate_p_and_q(L, accuracy):

	res = None
	p = q = seed = counter = 0

	while not res:
		q, seed, g = generate_q_and_seed()
		while not prime(q, accuracy):
			q, seed, g = generate_q_and_seed()

		res = from_step_seven((q, L, g, accuracy, seed))

	p, counter = res

	# save seed and counter somewhere
	return (p, q)

def generate_q_and_seed():
	seed = random.getrandbits(160)
	g = len(bin(seed)[2:])
	U = sha_ex(seed) ^ sha_ex((seed + 1) % 2 ** g)
	q = U | 2 ** 159 | 1

	return (q, seed, g)

def sha_ex(obj):
	return int(sha1(bytes(bin(obj).encode('ascii')[2:])).hexdigest(), 16)

def from_step_seven(data_tuple, counter = 0, offset = 2):
	q, L, g, accuracy, seed = data_tuple

	n = (L - 1) // 160
	b = (L - 1) % 160
	
	V = [sha_ex((seed + offset + k) % 2 ** g) for k in range(0, n + 1)]
	W_list = list(map(lambda Vk, k: Vk * 2 ** (160 * k), V, range(0, n)))
	W_list.append((V[n] % 2 ** b) * 2 ** (160 * n))
	W = sum(W_list)
	X = W + 2 ** (L - 1)
	c = X % (2 * q)
	p = X - c + 1

	if p >= 2 ** (L - 1) and prime(p, accuracy):
		return (p, counter)
	else:
		counter += 1
		offset += n + 1
		if counter >= 4096:
			return None
		else:
			return from_step_seven(data_tuple, counter, offset)
