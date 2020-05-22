#!/bin/bash -e
# -e means exit if any command fails
# This script helps to create a dump of the database in case of new changes
# Just change the credentials and the path to your local git repository and run it with
# zsh mycookin-dump-db.sh
DBHOST=localhost
DBUSER=YOUR_USER
DBPASS=YOUR_PASSWORD # do this in a more secure fashion
DBNAME=Recipes
GITREPO=PATH_TO_YOUR_REPO
cd $GITREPO
mysqldump -h $DBHOST -u $DBUSER -p$DBPASS  $DBNAME > $GITREPO/recipes.sql # if you don't want to include the data, add -d flag before $DBNAME
