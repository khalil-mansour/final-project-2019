import json
import sys
import pprint


def get_json(path):
	# open the json file
	with open(path, "r") as f:
		datastore = json.load(f)
	return datastore

def map_values(params, data):
	for key, value in params:
		data["env"][key] = value

def post_json(path, datastore):
	# write to json file
	with open(path, "w") as f:
		json.dump(datastore, f, indent=4)

def main(**kwargs):
	# path
	path = "Web.Api/appsettings.json"
	# fetch json data
	data = get_json(path)
	# get all  kvp params
	params = list(kwargs.items())
	# map the values
	map_values(params, data)
	# update the file
	post_json(path, data)

if __name__ == "__main__":
	main(**dict(arg.split('=') for arg in sys.argv[1:]))