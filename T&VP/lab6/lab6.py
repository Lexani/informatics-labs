import random
import primes

p, q = primes.generate_p_and_q(512, 16)
print('P = {}\nQ = {}'.format(p, q))
