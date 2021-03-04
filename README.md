# ITA (Image to ASCII) Converter
#### I'm aware that I'm not the first to make this, this is just a fun side-project I'm probably gonna work a bit on and then never touch it again.

#### The "brain" of the app is located in `src/js/imageToAscii.js`




QnA:

1. How does it work?
> I'm using an image processing npm package called JIMP (JavaScript Image Manipulation Program) that iterates through each pixel in the given image and runs the next formula: `numberOfCharacters / 255 * (0.2126 * R + 0.7152 * G + 0.0722 * B)`, and choose the character according to the solution to that formula (`characters[solution]`).
2. Why is the release file so big for such a simple app?
> Electron. I might make another version using a different language, probably WPF.
3. Can I add more characters or change the current ones?
> Yes. They're located in `chars.json`.
4. What can I do with the App/Source Code?
> as the `LICENSE` suggets, basically anything. Just don't claim it as yours, and if you do modify it, keep it open source.
5. What is that app icon?
> Didn't know what icon to give it and I didn't really want it to have the default Electron icon, so I used my Discord Profile Picture :D
