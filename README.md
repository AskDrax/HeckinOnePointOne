# HeckinOnePointOne
Tinkering around with the Powers of 1.1!

HeckinOnePointOne is a WPF+XAML re-write of an old Winforms project that calculates the Powers of 1.1.

It's a strange story how I even got interested in 1.1 and it's Powers, but it's Heckin interesting!

The gist of it:
```
1.1^0	 = 1
1.1^1	 = 1.1
1.1^2	 = 1.21
1.1^3	 = 1.331
1.1^4	 = 1.4641	
1.1^5	 = 1.61051
1.1^6	 = 1.771561
...
```

First, notice every Power ends in the digit '1'.

Also, every Power up to 1.1^4 is a Palindrome (the same forwards and backwards), and actually in a high enough base, *every* power would be a Palindrome.

Next, early on I discovered a simple trick to calculate each power:

1) Start with 1.1^0 = 1

2) Move the decimal point one place to the left, and add that to the current Power:

```
  1
+ 0.1
= 1.1

  1.1
+ 0.11
= 1.21

  1.21
+ 0.121
= 1.331

etc...
```
After working out just the first few Powers, I was surprised to notice how eerily close 1.1^5 (1.61051) is to the mathematical constant Phi (1.61803...)! (It's actually less by only ~ 0.00752...)

And Heckin woah, the very next Power, 1.1^6 (1.771561) is even *closer* to the Square Root of Pi (1.772454...) (Less by only ~0.000893...)!

And again, check out 1.1^12 (3.138428376721) which of course is near Pi (3.14159265...), (Less by ~0.00316427...)

Now, I'm not a big Math guy, but I figured surely this is something pretty cool, maybe?

So I kept digging further, and eventually wrote a super basic WinForms app to calculate Powers of 1.1...

The old project was quite inefficient and had an arbitrary maximum of 1.1^500 (which took ~8 seconds on my current Rig).


Lately I'd really been wanting to do a nice re-write in WPF/XAML and see if I can optimize the algorithm, so I began this project here a couple days ago.

So here's the new version, which has no fixed maximum, and it'll calculate all the Powers from 1.1^0 to 1.1^500 in ~22ms (1.1^10000 in less than 8 seconds :P)...

![Screenshot](http://www.askdrax.com/HeckinOnePointOne/NewFasterWPFVersion.png)

It gets slower the more Powers you want, but I think a lot of the slowdown comes from populating the ListBox with so many Heckin Long strings.

I might try truncating values outside the visible scroll range, and dynamically revealing all the digits within the scroll range...

...

Each Power is calculated with full precision and displayed as a String.

Presently the CalculatePowers() method relies on CalculateFromPrevious(), so it will always start with 1.1^0 = 1, and Calculate each power one by one using the previous powers value.

...

If I can work out an algorithm for generating a single arbitrary Power without depending on previous powers, I'll definitely implement it.

For now I'm working on adding XAML for displaying some details for the selected Power(s).


Beyond that I'm hoping to implement dynamic LINQ queries to do some Heckin neat analysis on the values, for example:

Get a Count of how many instances there are of each digit in a single Power or a selected range of Powers.

Find the first Power that contains a specific string of digits.

Get a List of every distinct string of digits and their Counts.

Find specific strings of digits "vertically", as in 3 consecutive values at a given digit position.

etc.

...

With what patterns I've already discovered "manually" and whatever I can discover with queries, I think I'll be able to improve the Calculation algorithm so that it works in chunks of digits at once instead of 1 digit at a time, and potentially storing and re-using calculations if it would be more efficient...

That's about it! #HeckinOnePointOne !





