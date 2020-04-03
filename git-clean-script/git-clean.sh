#!/usr/bin/env bash

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
  echo -e `git show --format="Deleting %ci %cr %an" $branch | head -n 1` \\t$branch; 
  echo -e `git branch -d $branch` \\t$branch; 
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
  echo -e `git show --format="· Are you still working on? %ci %cr %an" $branch | head -n 1` \\t$branch; 
  # echo "git branch -D $branch"
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
  #author="git show --format=\"%an\" $branch | head -n 1"
  #authorName=`eval $author`
  #if [[ $authorName == "Juan Carrey" ]]
  #then
	echo -e `git show --format="Merged remote %ci %cr %an" $branch | head -n 1` \\t$branch;  
    echo "git push --delete origin $branch"
  #fi
done


git remote prune origin

echo ""
echo ""
echo "=============================="
echo "      Cleanup complete"
echo "=============================="
echo ""
echo ""