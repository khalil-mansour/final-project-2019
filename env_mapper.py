import json

def get_json(path):
	#open the json file
	with open(path, "r") as f:
		datastore = json.load(f)
	return datastore

def main():
	data = get_json("Web.Api/appsettings.json")
	


if __name__ == "__main__":
	main()