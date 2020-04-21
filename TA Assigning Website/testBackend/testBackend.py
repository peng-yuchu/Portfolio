"""
This file contains a small subset of the tests we will run on your backend submission
"""

import unittest
import os
import testLib

class TestRoutes(testLib.ProjectTestLib):

    ###
    ### THESE ARE THE ACTUAL TESTS
    ###
    def testAdd1(self):
        """
        Test adding one smile
        """
        respCreate = self.makeRequest("/api/newuser", method="POST",
                                    data = { 'username' : 'test123@wsu.edu',
                                             'password' : 'test123',
                                             'title' : 'Student',
                                             'name' : 'test',
                                             'lastname' : 'testing',
                                             'wsuid' : '12345678',
                                             'phone' : '2067162829'
                                             })

        self.assertEqual('test123@wsu.edu', respCreate['user']['username'])
        self.assertEqual('test123', respCreate['user']['password'])

        # Now read the smiles
        respGet = self.getUser(order='test123@wsu.edu')
        self.assertEqual(1, len(respGet['users']))
        self.assertEqual(respCreate['user']['id'], respGet['users'][0]['id'])

    def testAdd2(self):
        """
        Test adding one smile
        """
        respCreate = self.makeRequest("/api/new_course", method="POST",
                                    data = { 'name' : 'CPTS121',
                                             'professor' : 'test',
                                             'prof_id' : '12345678',
                                             'lab_section' : '01',
                                             'prof_email' : 'test@wsu.edu'
                                             })

        self.assertEqual('CPTS121', respCreate['course']['name'])
        self.assertEqual('test@wsu.edu', respCreate['course']['prof_email'])

        # Now read the smiles
        respGet = self.getCourse(course_name='CPTS121',prof_email='test@wsu.edu')
        self.assertEqual(1, len(respGet['courses']))
        self.assertEqual(respCreate['course']['id'], respGet['courses'][0]['id'])

    def testAdd3(self):
        """
        Test adding one smile
        """
        respCreate = self.makeRequest("/api/apply", method="POST",
                                    data = { 'name' : 'test',
                                             'lastname' : 'tester',
                                             'course' : 'CPTS121',
                                             'labsection' : 'Lab Section 01',
                                             'wsuid' : '12345678',
                                             'phone' : '20976378271',
                                             'profname' : 'test@wsu.edu'
                                             })

        self.assertEqual('test', respCreate['applied']['name'])
        self.assertEqual('test@wsu.edu', respCreate['applied']['profname'])

        # Now read the smiles
        respGet = self.getApplicant(course_name='CPTS121',professor_name='test@wsu.edu')
        self.assertEqual(1, len(respGet['applied']))
        self.assertEqual(respCreate['applied']['id'], respGet['applied'][0]['id'])


if __name__ == '__main__':
    unittest.main()