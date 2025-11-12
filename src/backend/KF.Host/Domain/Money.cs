namespace KF.Host.Domain;

public readonly record struct Money(Currency Currency, decimal Value);