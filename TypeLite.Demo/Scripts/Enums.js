var Eshop;
(function (Eshop) {
    /**
     * Customer Kind
     */
    var CustomerKind;
    (function (CustomerKind) {
        /**
         * Corporate customer
         */
        CustomerKind[CustomerKind["Corporate"] = 1] = "Corporate";
        /**
         * Individual customer
         */
        CustomerKind[CustomerKind["Individual"] = 2] = "Individual";
    })(CustomerKind = Eshop.CustomerKind || (Eshop.CustomerKind = {}));
})(Eshop || (Eshop = {}));
//# sourceMappingURL=Enums.js.map