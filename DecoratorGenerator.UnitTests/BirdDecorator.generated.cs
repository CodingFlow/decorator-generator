﻿// <auto-generated/>
namespace SampleLibrary;

public abstract class BirdDecorator : IBird
{
    private IBird bird;

    protected BirdDecorator(IBird bird) {
        this.bird = bird;
    }



    public virtual string Chirp() {
        return bird.Chirp();
    }
}
