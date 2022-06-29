import json

def parse():
    f = open("data", "r")
    jsonFile = {"result":{}}
    for line in f:
        data = json.loads(line)
        jsonFile.get("result").get
        