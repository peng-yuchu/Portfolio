"""
A library for functional testing of the backend API
"""

import unittest
import traceback
import http.client
import sys
import os
import json


class ProjectTestLib(unittest.TestCase):
    """
    Superclass for functional tests. Defines the boilerplate for the tests
    """

    # Lookup the name of the server to test
    serverToTest = "localhost:5000"
    VERBOSE = 0


    def makeRequest(self, url, method="GET", data={ }):
        """
        Make a request to the server.
        @param url is the relative url (no hostname)
        @param method is either "GET" or "POST"
        @param data is an optional dictionary of data to be send using JSON
        @result is a dictionary of key-value pairs
        """

        printHeaders = (self.VERBOSE == 1)
        headers = { }
        body = ""
        if data is not None:
            headers = { "content-type": "application/json", "Accept": "application/json" }
            body = json.dumps(data)

        try:
            self.conn.request(method, url, body, headers)
        except Exception as e:
            if str(e).find("Connection refused") >= 0:
                print ("Cannot connect to the server ",RestTestCase.serverToTest,". You should start the server first, or pass the proper TEST_SERVER environment variable")
                sys.exit(1)
            raise

        self.conn.sock.settimeout(100.0) # Give time to the remote server to start and respond
        resp = self.conn.getresponse()
        data_string = "<unknown"
        try:
            if printHeaders:
                print("\n****")
                print("  Request: ",method," url=",url," data=",str(data))
                print("  Request headers: ",str(headers))
                print("")
                print("  Response status: ",str(resp.status))
                print("  Response headers: ")
                for h, hv in resp.getheaders():
                    print("    ",h,"  =  ",hv)

            self.assertEqual(200, resp.status)
            data_string = resp.read().decode()
            if printHeaders:
                print("  Response data: ",data_string)
            # The response must be a JSON request
            # Note: Python (at least) nicely tacks UTF8 information on this,
            #   we need to tease apart the two pieces.
            self.assertTrue(resp.getheader('content-type') is not None, "content-type header must be present in the response")
            self.assertTrue(resp.getheader('content-type').find('application/json') == 0, "content-type header must be application/json")


            data = json.loads(data_string)
            return data

        except:
            # In case of errors dump the whole response,to simplify debugging
            print("Got exception when processing response to url="+url+" method="+method+" data="+str(data))
            print("  Response status = "+str(resp.status))
            print("  Response headers: ")
            for h, hv in resp.getheaders():
                print("    "+h+"  =  "+hv)
            print("  Data string: ",data_string)
            print("  Exception: ",traceback.format_exc ())
            if not printHeaders:
                print("  If you want to see the request headers, use VERBOSE=1")


    def setUp(self):
        self.conn = http.client.HTTPConnection(self.serverToTest, timeout=1)
        # We empty the smile space, before we start the test
        
        

    def tearDown(self):
        self.conn.close ()


    ## NOW SOME METHODS THAT ARE USEFUL FOR TESTING SMILES
    def getUser(self,order=None):
        """
        Helper function to get some smiles
        """
        url = '/api/login'
        if order is not None:
            url += '?user_name='+order

        print(url)

        respData = self.makeRequest(url, method='GET')
        return respData

    def addUser(self,id):  #id is not optional
        """
        Helper function to like smile
        """
        url = '/api/newuser'

        print(url)

        respData = self.makeRequest(url, method='POST')
        return respData

    def getCourse(self,course_name=None,prof_email=None):

        url = '/api/courses'
        if course_name is not None:
            url += '?course_name='+course_name+'&professor_name='+prof_email
        print(url)

        respData = self.makeRequest(url, method='GET')
        return respData

    def addCourse(self):

        url = '/api/new_course'
        print(url)
        respData = self.makeRequest(url, method="POST")
        return respData

    def getApplicant(self,course_name=None,professor_name=None):

        url = '/api/applicants'
        
        if course_name is not None:
            url += '?course_name='+course_name+'&professor_name='+professor_name
        print(url)
        respData = self.makeRequest(url, method='GET')
        return respData


    def addApplicant(self):
        url = '/api/apply'
        print(url)
        respData = self.makeRequest(url, method="POST")
        return respData

    def assertSuccessResponse(self,
                              respData,
                              msg=None):
        """
        Check that the response is not an error response
        """
        self.assertTrue(respData != None, 'Response is None')
        self.assertEqual(1, respData["status"], msg)

