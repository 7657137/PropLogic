Liam McNamara 7657137

FEATURES
+Truth table dedustion
+Forward chaining deduction
+Back Chaining deduction
+Console Interface
+Deduction chain for both back and forward chaining
+Reads knowledge bases in horn notation from a text file
+Reads OR operator "|" from text file
BUGS
*Will only accept two variables and one operator on the left hand side of a statement
*Will include all variables entailed in finding the goal in forward chaining
MISSING
-Only takes horn notation knowledge bases
-No NOT operator

TEST CASES

ACKNOWLEDGEMENTS
This team would like to thank the teaching staff for sharing the knowledge and explaining the concepts involved in this task.
We would also like to thank the other students in the unit for discussing these topics and sharing and critiquing ideas.

NOTES
*The TELL section of the knowledgebase only reads the line directly below the TELL command
*The ASK section does likewise
*The program needs more than 1 arument to run but if the two or more given are invalid it defaults to TT method on test1.txt
*The forward chaining method operates in "sweeps" where it checks all variables that it can. Vars entailed in the same sweep as the goal will be listed, even if they aren't related.

SUMMARY REPORT
