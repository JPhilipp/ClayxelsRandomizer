# Clayxels Randomizer

This is a simple tool to randomize your Clayxel clayObject positions, rotations, colors and more over time... for inspiration, creating variations, and fun!

[![](https://img.youtube.com/vi/FucO_pYHdtw/0.jpg)](https://www.youtube.com/watch?v=FucO_pYHdtw)

**To get started:**
1. Grab the terrific **[Clayxels library](https://andrea-intg.itch.io/clayxels)** and add it somewhere to the project.
2. Add the component **ClayxelRandomizer** to your Clayxel or Clayxels container, and hit play.

You will now see your Clayxel randomized over time (you can adjust the speed in the inspector). You can hit the Return key to stop the auto-forwarding, and step manually via the Space key.

---

A **ClayObjectRandomizationStrength** component is automatically added, in it you can set specific randomization strengths. For instance, increase the base strength factor from 1 to 6 to get much more variation. You can also tune specific randomization properties like position to increase just the position variation -- or to disable it by setting it to 0.

The ClayObjectRandomizationStrength component can also be added to specific ClayObjects, the values are then multiplied with the base values. For instance, if you have a face Clayxel but never want your eyes to be randomized, you can set the base strength on just this gameObject to 0.

---

Hope you enjoy!
