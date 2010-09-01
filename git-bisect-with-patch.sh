#!/bin/sh

function clean_up_and_exit()
{
	# We have to rewind the cherry-picked commit that adds the reproduction to the current bisect HEAD.
	silence=$(git reset --hard $1)

	# Clean up.
	silence=$(git clean -d -f)

	echo "<<<<<<<<<<<< Exit: $2"
	exit $2
}

# Cherry-pick the reproduction for the bug. `retrofit-reproduction` is a Git tag.
silence=$(git cherry-pick retrofit-reproduction 2>/dev/null)

if [ $? -ne 0 ]; then
	echo ">>>>>>>>>>>> Cherry-pick failed"
	
	# Rewind the failed cherry-pick.
	# The special exit code 125 should be used when the current source code cannot be tested.
	clean_up_and_exit "HEAD" 125
fi 

echo ">>>>>>>>>>>> Building"

./build.cmd
build_successful=$?

echo "<<<<<<<<<<<< Build result: $build_successful"

# Rewind the cherry-pick.
# Exit with the return code of the build script.
clean_up_and_exit "HEAD~1" $build_successful