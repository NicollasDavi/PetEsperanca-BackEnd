import React, { createContext, useContext, useState, ReactNode } from 'react';

interface SearchContextData {
  searchTerm: string;
  setSearchTerm: (term: string) => void;
}

const SearchContext = createContext<SearchContextData | undefined>(undefined);

export const useSearch = () => {
  const context = useContext(SearchContext);
  if (!context) {
    throw new Error('useSearch must be used within a SearchProvider');
  }
  return context;
};

export const SearchProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [searchTerm, setSearchTerm] = useState('');

  return (
    <SearchContext.Provider value={{ searchTerm, setSearchTerm }}>
      {children}
    </SearchContext.Provider>
  );
};
