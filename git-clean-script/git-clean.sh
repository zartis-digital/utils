#!/usr/bin/env bash

# TODO Parse params in a better way....
DEL_LOCAL=$(([ "$2" = "-l" ] || [ "$3" == "-l" ] || [ "$4" == "-l" ] || [ "$5" == "-l" ]) && echo "YES" || echo "NO")
DEL_REMOTE=$(([ "$2" = "-r" ] || [ "$3" == "-r" ] || [ "$4" == "-r" ] || [ "$5" == "-r" ]) && echo "YES" || echo "NO")
VERBOSE=$(([ "$2" = "-v" ] || [ "$3" == "-v" ] || [ "$4" == "-v" ] || [ "$5" == "-v" ]) && echo "YES" || echo "NO")
SHOW_HELP=$(([ "$1" = "-h" ] || [ "$2" = "-h" ] || [ "$3" == "-h" ] || [ "$4" == "-h" ] || [ "$5" == "-h" ]) && echo "YES" || echo "NO")

if [ "$SHOW_HELP" = "YES" ]; then
    echo ""
    echo "=============================="
    echo "    Available Options"
    echo "=============================="
    echo ""
    echo " -v    : Verbose: Displays commands to remove remote not merged branches "
    echo " -l    : Remove Local: Deletes local merged branches otherwise dry-run "
    echo " -r    : Remove remote: Deletes merged remotes branches, otherwise dry-run "
    echo " -h    : Displays the help "
    echo ""
    echo ""
    echo "=============================="
    echo " Sample usages: "
    echo "=============================="
    echo ""
    echo " ./git-clean.sh \"Juan Carrey\" -v "
    echo " ./git-clean.sh \"Juan Carrey\" -v -l -r "
    echo " ./git-clean.sh -h "
    echo ""
    echo ""
    exit 0
fi

if [ "" == "$1" ]; then
    echo " Author required for filtering remote branches "
    exit 1
fi

git checkout master  > /dev/null 2>&1
git fetch -p  > /dev/null 2>&1

echo ""
echo ""
echo "=============================="
echo " Local merged branches "
echo "=============================="
echo ""
echo ""
echo ""

for branch in `git branch --merged | grep -vwE "(\*|master|release|rc-.*)"`; 
do 
  echo -e `git show --format="Deleting %ci %cr %an" $branch | head -n 1` $branch;

  if [ "$DEL_LOCAL" = "YES" ]; then
    echo -e `git branch -d $branch` $branch;
  else
    echo `git branch -d $branch` $branch;
  fi
done

echo ""
echo ""
echo "=============================="
echo "Local NOT merged branches [CAREFUL]"
echo "=============================="
echo ""
echo ""
echo ""

for branch in `git branch --no-merged | grep -vwE "(\*|master|release|rc-.*)"`; 
do 
  echo -e `git show --format="· Are you still working on? %ci %cr %an" $branch | head -n 1` $branch;

  if [ "$VERBOSE" = "YES" ]; then
    echo "git branch -D $branch"
  fi
done | sort -r
  
echo ""
echo ""
echo "======================================"
echo " Remote branches"
echo "======================================"
echo ""
echo ""
echo ""

for branch in `git branch -r --merged | grep -v HEAD | grep -vwE "(\*|master|release|rc-.*)"`; do 
  author="git show --format=\"%an\" $branch | head -n 1"
  authorName=`eval $author`
  if [[ $authorName == $1 ]]
  then
    if [ "$DEL_REMOTE" = "YES" ]; then
	    echo -e `git show --format="Merged remote %ci %cr %an" $branch | head -n 1` $branch;
        echo -e "git push --delete origin $branch"
    else
        echo `git show --format="Merged remote %ci %cr %an" $branch | head -n 1` $branch;

        if [ "$VERBOSE" = "YES" ]; then
            echo "git push --delete origin $branch"
        fi
    fi

  else
  	if [ "$VERBOSE" = "YES" ]; then
        echo `git show --format="Merged remote %ci %cr %an" $branch | head -n 1` $branch;
        echo "git push --delete origin $branch"
    fi
  fi
done

if [ "$DEL_REMOTE" = "YES" ]; then
    git remote prune origin
fi

echo ""
echo ""
echo "=============================="
echo "      Cleanup complete"
echo "=============================="
echo ""
echo ""
