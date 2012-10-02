import sys
import os
import threshold_scheme
from time import time
from random import shuffle

def restart():
	python = sys.executable
	os.execl(python, python, * sys.argv)

# sharing part
secret = 0; n = 0; t = 0
print('Tip: 9 repeated 77 times is 256 bit')

try:
	secret = int(input('Enter secret, at least 256 bit (number only): '));
	while len(bin(secret)[2:]) < 256:
		secret = int(input('Too short secret, try again (current: {} bit): '.format(len(bin(secret)[2:]))))

	n = int(input('Enter the number of parts that secret needs to be splitted (n, 3 < n < 20): '))
	while not 3 < n < 20:
		n = int(input('Possible n values: 3 < n < 20. Try again: '))

	t = int(input('Enter the minimum number of parts that can recover secret (t, 2 < t < {}): '.format(n)))
	while not 2 < t < n:
		t = int(input('Possible t values: 2 < t < {}. Try again: '.format(n)))

except ValueError:
	print('Your input should contain numbers only! Program restarts\n')
	restart()

else:
	print('Secret length is {} bit'.format(len(bin(secret)[2:])))

	print('Generating secret fragments...')
	fragments = threshold_scheme.share_secret(secret, t, n)

	print('Shares:')
	i = 0
	for f in fragments:
		i += 1
		print('Share #{}:\np:\t\t\t{}\ncoprime:\t{}\nshare:\t\t{}\n'.format(i, f['p'], f['d'], f['k']))

	# recovery part	
	print('\n--- recovery of secret ---')
	# choosing any t parts can recover secret
	shuffle(fragments)
	recovery_set = fragments[:t]
	print('Recovery set:')
	i = 0
	for f in recovery_set:
		i += 1
		print('Recovery part #{}:\np:\t\t\t{}\ncoprime:\t{}\nshare:\t\t{}\n'.format(i, f['p'], f['d'], f['k']))

	start_time = time()
	encrypted_secret = threshold_scheme.recover_secret(recovery_set)
	elapsed_time = time() - start_time
	if encrypted_secret == secret:
		print('--- SUCCESS ---\nTime elapsed: {}\nSecret:\n{}'.format(elapsed_time, encrypted_secret))
	else:
		print("--- ERROR Decrypted secret doesn't equal encrypted secret ---\nTime elapsed: {}\nSecret:\n{}\n".format(elapsed_time, encrypted_secret))

if input('Enter "exit" to exit. Any other input will restart program: ').lower() != 'exit':
	restart()
