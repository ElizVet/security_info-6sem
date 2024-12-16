#pip install stegano
#pip install wheel steganocryptopy

from stegano import lsb

secret = lsb.hide("pic.bmp", "SEK")
secret.save("pic_secret.bmp")

result = lsb.reveal("pic_secret.bmp")
print(result)

#from steganocryptopy.steganography import Steganography

# Steganography.generate_key("")
# secret = Steganography.encrypt("key.key", "img/pic.bmp", "secret_message.txt")
# secret.save("pic_secret_with_key.bmp")

# result = Steganography.decrypt("key.key", "pic_secret_with_key.bmp")
# print(result)
