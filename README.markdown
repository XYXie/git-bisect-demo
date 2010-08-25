git-bisect Demo
======================================================================

*NOTE: I typically run git under Cygwin. If you're using msysgit your mileage may vary.*

This project demos how to use [git-bisect](http://www.kernel.org/pub/software/scm/git/docs/git-bisect.html). The project contains a simple calculator with a broken adder.

Your task is to find the commit that introduced the bug.

## What's bisecting?

Bisecting is the process of finding a commit that introduced a bug. The process is roughly as follows:

- Define a range of revisions from a "last-known-good" to a broken revisison.
- Select a commit in the revision range by running a divide and conquer algorithm.
- Check if the commit contains the bug.
- Rinse and repeat step 2 and 3 until the bug-introducing commit is found.

The awkward part is to check commits if they contain a bug. Luckily, this can be automated using the build script you have in place ... which you do, right?

## Usage

The repository contains two branches, `master` and `retrofit`.

Both branches have a broken calculator:

- The `master` branch includes a test for the calculator's `Add` method, `When_two_numbers_are_added`. The test has been introduced at the point in time when `Add` has been implemented (think TDD).

- The `retrofit` branch *initially* did not a test for the broken calculator. This usually the case when developers don't subscribe to TDD or when the bug was not covered by the tests that were written by the time the feature was implemented. What you would usually do is to write a reproduction for the bug in the form of a test. For your convenience I've added the reproducing test in the most recent commit in `retrofit`, `When_two_numbers_are_added_with_the_broken_implementation`.

Each branch contains:

- A buggy `Calculator`.
- A build script (`rakefile.rb` along with a minimal version of Ruby) that is used to build the calculator.
- A helper script, `build.cmd` that invokes `rake`.
- Tags denoting the working and broken revisions:
  - `working` and `master-broken` for the `master` branch
  - `working` and `retrofit-broken` for the `retrofit` branch

### Bisecting in `master`

After cloning the repository start bisecting the `working`..`master-broken` revision range. 
   
    git bisect start master-broken working
    git bisect run ./build.cmd

Now watch git finding the commit that introduced the bug in the addition logic.

### Bisecting in `retrofit`

Bisecting without having a proper reproduction in the commits containing the bug is a bit more complicated.

You will have to retrofit the bug's reproduction to historical commits. In this example, we use cherry-picking to apply the reproduction to every commit in the `working`..`retrofit-broken` revision range. A little helper script, `git-bisect-with-patch.sh`, does the cherry-picking, build script invocation and clean-up for us.

Switch to the `retrofit` branch and start bisecting the `working`..`retrofit-broken` revision range.
   
    git checkout -b retrofit --track origin/retrofit
    cp ./git-bisect-with-patch.sh /tmp/
    
    git bisect start retrofit-broken working
    git bisect run /tmp/git-bisect-with-patch.sh
    
    rm /tmp/git-bisect-with-patch.sh