﻿using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments.Events;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity
{
    private Apartment(
        Guid id,
        DateTime createdAt,
        string name,
        string description,
        Address address,
        Money price,
        List<Rule> rules,
        List<Amenity> amenities)
        : base(id, createdAt)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        Rules = rules;
        Amenities = amenities;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Address Address { get; private set; }

    public Money Price { get; private set; }

    public List<Rule> Rules { get; private set; }

    public List<Amenity> Amenities { get; private set; }

    public static Result<Apartment> Create(
        string name,
        string description,
        Address address,
        Money price,
        List<Rule>? rules = null,
        List<Amenity>? amenities = null)
    {
        var apartment = new Apartment(
            Guid.CreateVersion7(),
            DateTime.Now,
            name,
            description,
            address,
            price,
            rules ?? [],
            amenities ?? []
        );

        apartment.RaiseDomainEvents(new ApartmentCreatedDomainEvent(apartment.Id));

        return Result.Success(apartment);
    }
}

