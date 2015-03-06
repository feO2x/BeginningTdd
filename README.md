# BeginningTdd
This repository contains examples that can be used to learn and teach Test-Driven-Development.

I recorded a screen-cast when I started creating the List<T> implementation with TDD - unfortunately I'm not finished yet. I will update this repo with the corresponding YouTube links to the videos once I'm finished.

Today (2015-02-18) I finished the implementation of List<T>. I will upload the videos I recorded soon.

## Mysterious bug in VS 2013 Update 4 Debug mode
Yesterday (2015-03-05) I saw that the test `TestTargetMustNotHoldAReferenceToItemsAfterCallingClear` fails in VS 2013 Update 4 in Debug mode (mysteriously, it passes in Release mode). I think it has nothing to do with xunit.net because I tried several versions and this very test method passes in VS 2015 CTP 6 in both configurations, Debug and Release. I created a Stack Overflow question that you can find [here](http://stackoverflow.com/questions/28889055/mysterious-array-keeps-reference-to-object-in-unit-tests).

If you encounter the error, too, I would advise you to run the tests in Release mode or skip the test (this info can be added to the `FactAttribute` of the test). I created a separate branch called *FailingReferenceTest* for this issue.
