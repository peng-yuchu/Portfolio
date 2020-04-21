/**
 *  Basic hash table implementation
 *   Aaron S. Crandall - 2017 <acrandal@gmail.com>
 *
 */

#ifndef __HASH_H
#define __HASH_H

#include <unordered_map>
#include <string>
#include <iostream>
#include <list>
#include <vector>
#include <iterator>

using namespace std;
/*
    private:
        void rehash();
        int hash_function(KEYTYPE key);
        
    public:
        bool insert(KEYTYPE key, VALTYPE val);
        bool contains(KEYTYPE key);
        int remove(KEYTYPE key);
        VALTYPE & find(KEYTYPE key);
        int size();            // Elements currently in table
        bool empty();          // Is the hash empty?
        float load_factor();   // Return current load factor
        void clear();          // Empty out the table
        int bucket_count();    // Total number of buckets in table
*/

template <typename KEYTYPE, typename VALTYPE>
class Hashtable
{
    private:
		int tablesize = 0, Numinsert = 0, bucketNum = 0;
		
		vector< list<VALTYPE> > hash_table;
        /**
         *  Rehash the table into a larger table when the load factor is too large
         */
        void rehash() 
		{
			vector<VALTYPE> keys;
			keys.resize(tablesize);
			int index = 0;
			tablesize *= 2;
			for (index = 2; index <= tablesize; index++) //finds next prime
			{
				if (tablesize % index != 0)
				{
					index = 2;
					tablesize++;
				}
			}

			list<Word>::iterator it;
			for (index = 0; index < tablesize; index++)
			{
				if (!hash_table[index].empty())
				{
					for (it = hash_table[index].begin(); it != hash_table[index].end(); it++)
					{
						if (!it->deleted) //saves all non deleted keys for hashing.
						{
							keys.push_back(*it);
						}
					}
				}
			}
			hash_table.empty();
			hash_table.resize(tablesize);
			while (!keys.empty()) //rehashes
			{
				this->insert(keys.back().myword, keys.back());
				keys.pop_back();
			}
		}

    

        /**
         *  Function that takes the key (a string or int) and returns the hash key
         *   This function needs to be implemented for several types it could be used with!
         */
        int hash_function(int key) {
            cout << " Hashing with int type keys." << endl;
			int value = 0;
			for (int ch : key)
			{
				value = 78 * value + ch * 2;
			}
			return (value % tablesize);
        }

        int hash_function(string key) {
            /*cout << " Hashing with string type keys." << endl;*/
			int value = 0;
			for (char ch : key)
			{
				value = 78 * value + ch;
			}
			return (value % tablesize);
        }

    public:
        /**
         *  Basic constructor
         */
        Hashtable( int startingSize = 101 )
        {
			this->tablesize = startingSize;
			hash_table.resize(tablesize);
			this->Numinsert = 0;
			this->bucketNum = 0;
        }

        /**
         *  Add an element to the hash table
         */
		bool insert(KEYTYPE key, VALTYPE val) {
			// Currently unimplemented
			int hash_key = hash_function(key);
			this->Numinsert++;
			if (hash_table[hash_key].front().myword == "")
			{
				hash_table[hash_key].push_back(val);
				this->bucketNum++;
			}
			else
			{
				hash_table[hash_key].push_back(val);
				this->bucketNum++;
			}
			return false;
		}
        /**
         *  Return whether a given key is present in the hash table
         */
        bool contains(KEYTYPE key) {
			int hash_key = hash_function(key);
			typename list<VALTYPE>::iterator plist;
			for (plist = hash_table[hash_key].begin(); plist != hash_table[hash_key].end(); plist++)
			{
				if (key == (*plist).myword)
				{
					if (plist->deleted)
					{
						return false;
					}
					return true;
				}
				
			}
			return false;
        }


        /**
         *  Completely remove key from hash table
         *   Returns number of elements removed
         */
        int remove(KEYTYPE key) {
            // Doesn't actually remove anything yet
			int hash_key = hash_function(key);
			int Removed = 0;

			if (hash_table[hash_key].front().definition == hash_table[hash_key].back().definition)
			{
				Removed = 1;
				hash_table[hash_key].front().deleted = true;
				this->Numinsert--;
			}
			else
			{
				typename list<VALTYPE>::iterator it;
				for (it = hash_table[hash_key].begin(); it != hash_table[hash_key].end(); it++)
				{
					Removed++;
					it->deleted = true;
					this->Numinsert--;
				}
			}
			return Removed;
        }

        /**
         *  Searches the hash and returns a pointer
         *   Pointer to Word if found, or nullptr if nothing matches
         */
        VALTYPE *find(KEYTYPE key) {
			int hash_key = hash_function(key);
			typename list<VALTYPE>::iterator plist;
			for (plist = hash_table[hash_key].begin(); plist != hash_table[hash_key].end(); plist++)
			{
				if (key == (*plist).myword)
				{
					return &(*plist);
				}
			}
			return nullptr;
        }

        /**
         *  Return current number of elements in hash table
         */
        int size() {
			return this->Numinsert;
        }

        /**
         *  Return true if hash table is empty, false otherwise
         */
        bool empty() {
			return (this->Numinsert == 0);
        }

        /**
         *  Calculates the current load factor for the hash
         */
        float load_factor() {
            //return _hash.load_factor();
			return (float)Numinsert / tablesize;
        }

        /**
         *  Returns current number of buckets (elements in vector)
         */
        int bucket_count() {
			return tablesize;
        }

        /**
         *  Deletes all elements in the hash
         */
        void clear() {
            // Does nothing yet
			hash_table.clear();
			hash_table.resize(101);
			tablesize = 0;
			bucketNum = 0;
			Numinsert = 0;
			
        }

};


#endif
