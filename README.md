# Clayxels Randomizer

This is a simple tool to randomize your Clayxel clayObject positions, rotations and such over time. For inspiration, creating variations, and fun.

**To get started, add the component ClayxelRandomizer to your Clayxel container, and hit play.** You will see your Clayxel randomized over time (you can adjust the speed in the inspector).

A **ClayObjectRandomizationStrength** component is automatically added, in it you can set specific randomization strengths. For instance, increase the base strength factor from 1 to 6 to get much more variation. You can also tune specific randomization properties like position to increase just the position variation -- or to disable it by setting it to 0.

The ClayObjectRandomizationStrength component can also be added to specific ClayObjects, the values are then multiplied with the base values. For instance, if you have a face Clayxel but never want your eyes to be randomized, you can set the base strength on just this gameObject to 0.

Hope you enjoy!