

 



/// <reference path="Enums.ts" />

declare module Eshop {

  /**
   * Customer information
   */
  interface Customer {

    /**
     * Email address.
     */
    Email: string;

    /**
     * Customer's kind.
     */
    Kind: Eshop.CustomerKind;

    /**
     * Customer name.
     */
    Name: string;

    /**
     * Customer's orders.
     */
    Orders: Eshop.Order[];

    /**
     * Customer is VIP member or not.
     */
    VIP: boolean;
  }

  /**
   * a order
   */
  interface Order {

    /**
     * created date
     */
    Created: Date;

    /**
     * products
     */
    Products: Eshop.Product[];

    /**
     * shipped date
     */
    Shipped: Date;

    /**
     * total price
     */
    TotalPrice: number;
  }

  /**
   * product
   */
  interface Product {

    /**
     * ID
     */
    ID: System.Guid;

    /**
     * name
     */
    Name: string;

    /**
     * price
     */
    Price: number;
  }
}
declare module System {

  /**
   * Represents a globally unique identifier (GUID).To browse the .NET Framework source code for this type, see the Reference Source.
   */
  interface Guid {
  }
}
declare module TypeLite.Demo.Models {

  /**
   * Shipping Service
   */
  interface IShippingService {

    /**
     * price
     */
    Price: number;
  }
}


