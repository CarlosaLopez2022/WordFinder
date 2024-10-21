Explanation:
1.	Class Structure: The WordFinder class takes a character matrix as input and stores it in an array.
2.	Finding Words: The Find method:
    o	Gathers all unique words from the matrix, both horizontally and vertically.
    o	Takes an instance of IWordStreamProvider, allowing the method to retrieve the word stream through dependency injection*
    o	Uses a HashSet<string> for the input word stream to ensure unique words are processed.
    o	Checks if each word from the stream exists in the matrix's collected words.
3.	Result: The method returns the top 10 unique words found, based on their presence in the matrix.
Performance Considerations:
•	The use of HashSet for storing and checking the existence of words ensures average time complexity for lookups.
•	By storing horizontal and vertical words in a single pass, the solution is optimized for both time and space.
*Benefits of Dependency Injection:
•	This approach allows for greater flexibility, as you can now easily swap out the word stream provider with different implementations, such as retrieving words from a database or an API without modifying the WordFinder class itself.
•	It promotes loose coupling, making the code easier to test and maintain
