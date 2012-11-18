## How to run

Just run scopely-challenge.exe on Windows. You'll
need a recent version of visual studio as I used 
Code Contracts.

No input required. I used the pdf samples as
parametrized input.

I made a BUNCH of assumptions:

- From the samples, it doesn't look like the author meant combinatorial.
  I think he meant permutations. I decided to follow the visual samples
  rather than following the exact meaning.

- I assumed combinations music-sports and sports-music are the exact same
  permutation. This is by design and lazyiness.

## LED Assisted Meta Edition (LAME) Display

- I wanted to do a graphviz proj but I don't think I'll be able to finish
  it on time. Plus I was hesitant to use it as it may count as an external
  library.

- So I opted by a very simple visual representation of the trees:
```
Depth Parent:Node 
```
i.e. ```/home``` is always the root folder in these samples, so it would
be represented as 0 :home