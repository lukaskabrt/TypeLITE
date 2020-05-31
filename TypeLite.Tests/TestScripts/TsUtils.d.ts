
 
 
 



/// <reference path="Enums.ts" />

declare module TypeLite.Tests.TestModels {
interface Employee extends TypeLite.Tests.TestModels.Person {
  Salary: number;
}
interface Person {
  Name: string;
  YearOfBirth: number;
  PrimaryAddress: TypeLite.Tests.TestModels.Address;
  Addresses: TypeLite.Tests.TestModels.Address[];
  PhoneNumber: string;
}
interface Address {
  Id: System.Guid;
  Ids: System.Guid[];
  Street: string;
  Town: string;
  AddressType: TypeLite.Tests.TestModels.ContactType;
  Shortkey: System.ConsoleKey;
  CountryID?: number;
  PostalCode: string;
}
}
declare module System {
interface Guid {
  Empty: System.Guid;
}
}


