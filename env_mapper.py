import json
import sys
import pprint
import io
import os
import configparser

def get_json(path):
	# open the json file
	with open(path, "r") as f:
		datastore = json.load(f)
	return datastore

def map_values(data, parser):
	# map the interpolated values
	for section in parser.sections():
		for option in parser.options(section):
			data[section][option] = os.path.expandvars(parser[section][option])

def post_json(path, datastore):
	# write to json file
	with open(path, "w") as f:
		json.dump(datastore, f, indent=4)

def main():
	parser = configparser.ConfigParser()
	parser.read('config.ini')
	# path
	path = "Web.Api/appsettings.json"
	# fetch json data
	data = get_json(path)
	# map the values
	map_values(data, parser)
	# update the file
	post_json(path, data)

if __name__ == "__main__":
	main()