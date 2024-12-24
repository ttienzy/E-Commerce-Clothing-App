import React, { createContext, useState } from 'react';

const CartContext = createContext();

export const CartProvider = ({ children }) => {
  const [count, setCount] = useState(0);

  return (
    <CartContext.Provider value={{count, setCount}}>
      {children}
    </CartContext.Provider>
  );
};

export default CartProvider;
export {CartContext};