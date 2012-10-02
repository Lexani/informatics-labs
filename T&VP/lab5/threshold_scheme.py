import fractions
import random
from functools import reduce
from math import sqrt
from math import log
from operator import mul

MERSENNE_POWERS =  (2, 3, 5, 7, 13, 17, 19, 31, 61, 89, 107, 127, 521, 607, 1279, 2203, 2281, 3217, 4253, 4423, 9689, 9941, 11213, 19937, 21701, 23209, 44497, 86243, 110503, 132049, 216091, 756839)

def share_secret(secret, m, n):
	# choosing of prime number which satisfies p > secret condition
	p = prime_after(secret)
	print('p = {}'.format(p))

	result = []

	# choosing of n pairwise coprime numbers
	d = choose_pairwise_coprime_modules(p, n)

	while min_m_product(d, m) <= p * max_m_product(d, m - 1):
		d = choose_pairwise_coprime_modules(p, n, d[1:])
	# print('Chosen coprime modules: {0}'.format(d))

	# choosing of random r value
	r = int(log(secret, random.choice(MERSENNE_POWERS[:9]))) + 1
	# print('r = {}'.format(r))

	# calculation of M'
	M_ = secret + r * p
	# print('M-dash = {}'.format(M_))

	# calculation of share parts
	k = [M_ % di for di in d]
	# print('k = {}'.format(k))

	result = list(map(lambda di, ki: {'p': p, 'd': di, 'k': ki}, d, k))

	return result

def prime_after(M):
	# M + 1 is needed to return p > M for M = 2 ** x - 1
	power = int(log(M + 1, 2))
	for mp in MERSENNE_POWERS:
		if mp > power:
			power = mp
			break
	# print('Chosen Mersenne power: {}'.format(mp))		
	return 2 ** mp - 1

def choose_pairwise_coprime_modules(p, n, d = []):
	# initializing list with random initial value if empty
	if (len(d) == 0):		
		# choosing d[0] close to p
		left_bound = p + 1
		right_bound = p + int(log(p, 2)) + 1
		d.append(random.randint(left_bound, right_bound))

	# filling d-list
	for i in range(len(d), n):
		di = max(d)
		gcds = d
		while not all(g == 1 for g in gcds):
			di += 1
			gcds = list(map(lambda x: fractions.gcd(x, di), d))
		d.append(di)

	return d

def min_m_product(d, m):
	""" product of m minimal items """ 
	return reduce(mul, sorted(d[:m]))

def max_m_product(d, m):
	""" product of m maximal items """
	return reduce(mul, sorted(d)[-m:])

def recover_secret(recovery_set):
	# extraction of d-part (coprimes) from recovery set
	d = [share_part['d'] for share_part in recovery_set]
	# print('extracted d-part = {}'.format(d))

	# product d
	D = reduce(mul, d)
	# print('D = П(d[i]) = {}'.format(D))

	# calculation of M[i] = D // d[i]
	M = [D // di for di in d]
	# print('M: {}'.format(M))

	# extraction of k-part (remainders) from recovery set
	k = [share_part['k'] for share_part in recovery_set]
	# print('extracted k-part = {}'.format(k))

	# calculation of y => M[i] * y[i] = 1 (mod d[i])
	y = list(map(lambda Mi, di: inverse_mod(Mi, di), M, d))
	# print('y = {}'.format(y))

	S = sum(list(map(lambda _x, _y, _z: _x * _y * _z, M, k, y)))
	# print('S = {}'.format(S))

	L = S % D
	# print('x is {}'.format(L))

	return L % recovery_set[0]['p']

def inverse_mod_slow(number, modulo):
	# print('{} (mod {})'.format(number, modulo))
	res = 1
	while not (number * res) % modulo == 1:
		res += 1
	return res

def inverse_mod(number, modulo):
	# print('finding x: x * {} = 1 (mod {})'.format(number, modulo))
	if fractions.gcd(number, modulo) != 1:
		print('ERROR: inverse can not be found')
		return None

	a = number
	b = modulo
	if number > modulo:
		a, b = b, a

	q = []
	while b:
		q.append(a // b)
		a, b = b, a % b
		
	# print('q: {}'.format(q))

	# magic starts
	q = q[:-1]
	k = continuant(q)
	# print('K({}): {}'.format(len(q), k))
	k2 = -k
	# double magic
	variants = [k, number - k, k2 % modulo]
	# print(variants)

	for var in variants:
		tmp = var * number % modulo
		if tmp == 1:
			return var
	# magic ends

	return None

def continuant(seq, j = -1, continuant_list = {}):
	if j < 0:
		return continuant(seq, len(seq), {})
	else:
		if j == 0:
			continuant_list[0] = 1
		if j == 1:
			continuant_list[1] = seq[0]
		if not j in continuant_list:
			continuant_list[j] = continuant(seq, j - 1, continuant_list) * seq[j - 1] + continuant(seq, j - 2, continuant_list)

		return continuant_list[j]