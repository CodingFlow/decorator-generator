﻿// <auto-generated/>
namespace SampleLibrary;

public abstract class LionPropertiesDecorator : ILionProperties
{
    private ILionProperties lionProperties;

    protected LionPropertiesDecorator(ILionProperties lionProperties) {
        this.lionProperties = lionProperties;
    }

    public virtual string Name { get => lionProperties.Name; }

    public virtual string Color { set => lionProperties.Color = value; }

    public virtual int Age { get => lionProperties.Age; set => lionProperties.Age = value; }


}