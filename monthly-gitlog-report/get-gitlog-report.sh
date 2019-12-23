#!/bin/bash
AUTHOR=$1
MONTHS_AGO=$2
REPORT_FOLDER=$3
ROOT_PROJECTS_FOLDER=$4
MONTH_BEFORE=$(( MONTHS_AGO + 1 ))
cd $ROOT_PROJECTS_FOLDER
for d in */ ; do
    PROJECT="$d"
    echo "$PROJECT $MONTH_BEFORE $MONTHS_AGO"
    TARGET_MONTH=`date -v-"$MONTHS_AGO"m +%m`
    cd $PROJECT
    PROJECT_NAME=${PWD##*/}
    git log --author="$AUTHOR" --since=$MONTH_BEFORE.months.ago --until=$MONTHS_AGO.month.ago --all > $REPORT_FOLDER/$AUTHOR-2019$TARGET_MONTH-$PROJECT_NAME.log
    cd ..
done