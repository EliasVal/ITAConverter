# ITA (Image to ASCII) Converter
#### I'm aware that I'm not the first to make this, this is just a fun side-project I worked on.



QnA:

1. How does it work?
##### Electron/JS
> I'm using an image processing npm package called JIMP (JavaScript Image Manipulation Program) that iterates through each pixel in the given image and runs the next formula: `(numberOfCharacters-1) / 255 * (0.2126 * R + 0.7152 * G + 0.0722 * B)`, and choose the character according to the solution to that formula (`characters[solution]`).
##### WPF/.NET
> I'm using `System.Drawing`, you might need to install it through the NuGet Package Manager if you want it in your project, from there, it's pretty much the same as Electron/JS.
2. Why is the release file so big for such a simple app?
> Electron. There's a WPF version available that weights ~2mb (Instead of Electron's 170mb)
3. Can I add more characters or change the current ones?
> Yes. For Electron/JS you append the array in `chars.json` and in WPF/.NET you append the `chars.txt` file with a new line and the character you want to add.
4. What can I do with the App/Source Code?
> as the `LICENSE` suggets, basically anything. Just don't claim it as yours, and if you do modify it, keep it open source.
5. What is that app icon?
> Didn't know what icon to give it and I didn't really want it to have the default Electron icon, so I used my Discord Profile Picture :D
