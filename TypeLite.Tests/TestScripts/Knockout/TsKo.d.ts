
 
 
 



/// <reference path="Enums.ts" />

declare module TypeLite.Tests.TestModels {
	interface Employee extends TypeLite.Tests.TestModels.Person {
		Salary: KnockoutObservable<number>;
	}
	interface Person {
		Name: KnockoutObservable<string>;
		YearOfBirth: KnockoutObservable<number>;
		PrimaryAddress: KnockoutObservable<TypeLite.Tests.TestModels.Address>;
		Addresses: KnockoutObservableArray<TypeLite.Tests.TestModels.Address>;
		PhoneNumber: KnockoutObservable<string>;
	}
	interface Address {
		Id: KnockoutObservable<System.Guid>;
		Ids: KnockoutObservableArray<System.Guid>;
		Street: KnockoutObservable<string>;
		Town: KnockoutObservable<string>;
		AddressType: KnockoutObservable<TypeLite.Tests.TestModels.ContactType>;
		Shortkey: KnockoutObservable<System.ConsoleKey>;
		CountryID?: KnockoutObservable<number>;
		PostalCode: KnockoutObservable<string>;
	}
}
declare module System {
	interface Guid {
		Empty: KnockoutObservable<System.Guid>;
	}
}


