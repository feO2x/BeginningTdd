# BeginningTdd
This repository contains examples that can be used to learn and teach Test-Driven-Development.

You can watch the video series for this repository on my YouTube channel:<br />
https://www.youtube.com/playlist?list=PLIMrZfX3DMVEBHaiZIeZSNYCRsLILsuGm

## Mysterious bug in VS 2013 Update 4 Debug mode
Yesterday (2015-03-05) I saw that the test `TestTargetMustNotHoldAReferenceToItemsAfterCallingClear` fails in VS 2013 Update 4 in Debug mode (mysteriously, it passes in Release mode). I think it has nothing to do with xunit.net because I tried several versions and this very test method passes in VS 2015 CTP 6 in both configurations, Debug and Release. I created a Stack Overflow question that you can find [here](http://stackoverflow.com/questions/28889055/mysterious-array-keeps-reference-to-object-in-unit-tests).

If you encounter the error, too, I would advise you to run the tests in Release mode or skip the test (this info can be added to the `FactAttribute` of the test). I created a separate branch called *FailingReferenceTest* for this issue.
