import json
import sys
import pprint
import os
from configparser import SafeConfigParser

def get_json(path):
	# open the json file
	with open(path, "r") as f:
		datastore = json.load(f)
	return datastore

def map_values(data, parser):
	for option in parser['CONNECTIONSTRING']:
		data["env"][option] = parser['CONNECTIONSTRING'][option]

def post_json(path, datastore):
	# write to json file
	with open(path, "w") as f:
		json.dump(datastore, f, indent=4)

def get_values():
	parser = SafeConfigParser(os.environ)
	parser.read('config.ini')

def main():
	# read ini file
	parser = get_values()
	# path
	path = "Web.Api/appsettings.json"
	# fetch json data
	data = get_json(path)
	# map the values
	map_values(data, parser)
	# update the file
	post_json(path, data)
	print(data)

if __name__ == "__main__":
	main()