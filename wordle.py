# This script takes an input string and prints out every permutation of
# characters in that string. It was written to help provide inspiration when
# stuck on a Wordle. When being used for that purpose, it's best to only use 5
# character strings. If you only know 2 of the letters, then you can pad the
# rest of the input with a `-` to get it to be 5 characters. Passing anything
# less than 5 characters will result in nothing being printed out.
#
# Example usage
# > python .\wordle.py oer--
# oer--
# oer--
# oe-r-
# oe--r
# oe-r-
# <output truncated for the example>

from itertools import permutations
import sys

input = sys.argv[1]
for element in permutations(input, 5):
    print ("".join(element))
