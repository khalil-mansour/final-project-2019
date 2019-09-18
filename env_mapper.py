import json
import sys
import pprint
import os
from configparser import ConfigParser


class EnvInterpolation(configparser.BasicInterpolation):
    """Interpolation which expands environment variables in values."""

    def before_get(self, parser, section, option, value, defaults):
        return os.path.expandvars(value)

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

def main():
	# read ini file
	parser = ConfigParser(interpolation=EnvInterpolation())
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