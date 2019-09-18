import json
import sys
import pprint
import io
import os
import configparser

class ExpandingParser(configparser.ConfigParser):

     def getexpanded(self, section, option):
             return self._get(section, os.path.expandvars, option)


def get_json(path):
	# open the json file
	with open(path, "r") as f:
		datastore = json.load(f)
	return datastore

def map_values(data, parser):
	for section in parser.sections():
		for option in parser.options(section):
			print(parser.get(section, option))
			print(parser.getexpanded(section,option))
			data["env"][option] = parser[section][option]

def post_json(path, datastore):
	# write to json file
	with open(path, "w") as f:
		json.dump(datastore, f, indent=4)

def main():
	parser = ExpandingParser()
	parser.read('config.ini')
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