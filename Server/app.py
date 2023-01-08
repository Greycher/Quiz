#To run;
#flask run -p 8080

from flask import Flask
from flask import request
from pathlib import Path

app = Flask(__name__)

@app.route("/leaderboard")
def leaderboard():
    if request.args.__contains__("page"):
        page = request.args.get("page", type = str);
        path = "leaderboards/" + page + ".json";
        if Path(path).is_file():
            file = open(path);
            return file.read()

        return "There is no leaderboard for page " + page + "!"

    return "Specify a leaderboard page index to get the page data. Example: \"localhost:8080/leaderboard?page=0\""
