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

The repository contains:

- The buggy `Calculator`.
- A build script (`rakefile.rb` along with a minimal version of Ruby) that is used to build the calculator.
- A helper script, `build.cmd` that invokes `rake`.
- Two tags, `working` and `broken` to denote, well, working and broken revisions.

### Bisecting

After cloning the repository start bisecting the application. 
   
    git bisect start broken working
    git bisect run ./build.cmd

Now watch git finding the commit that introduced the bug in the addition logic.