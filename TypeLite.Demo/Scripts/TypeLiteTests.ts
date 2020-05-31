/// <reference path="TypeLite.d.ts" />

module Eshop {
    class CustomerImp {
        Name: string;
        Email: string;
        VIP: boolean;
        Kind: Eshop.CustomerKind;
        Orders: Eshop.Order[];
    };

    var customerObj: Eshop.Customer = new CustomerImp();
    customerObj.Kind = CustomerKind.Individual;
}

var test = Eshop.CustomerKind.Corporate;