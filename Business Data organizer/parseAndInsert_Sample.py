import json
import psycopg2

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def int2BoolStr (value):
    if value == 0:
        return 'False'
    else:
        return 'True'

def insert2BusinessTable():
    #reading the JSON file
    with open('yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='test' user='postgres' host='localhost' port='8212' password='123'")
        except:
            print('Unable to connect to the database!')

        cur = conn.cursor()

        while line:
            if(count_line%1000 == 0):
                print("Running business table")
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO Business(business_id, name, address, state, city, postal_code, latitude, longitude, stars, checkin_count, tip_count, is_open) " \
                      "VALUES ('" + data['business_id'] + "','" + cleanStr4SQL(data["name"]).replace("'","''") + "','" + cleanStr4SQL(data["address"]) + "','" + \
                      cleanStr4SQL(data["state"]) + "','" + cleanStr4SQL(data["city"]) + "'," + data["postal_code"] + "," + str(data["latitude"]) + "," + \
                      str(data["longitude"]) + "," + str(data["stars"]) + ", 0 , 0 ,"  +  str(data["is_open"]) + ");"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to businessTABLE failed!")
                print(sql_str)

            conn.commit()

            categories = data["categories"].split(', ')
            for c in categories:
                sql_str = "INSERT INTO Categories(business_id, category_name) " + " VALUES ('" + data['business_id'] + "','" + c.replace("'","''") + "');"
                try:
                    cur.execute(sql_str)
                except:
                    print("Insert to categoryTable failed!")
                    print(sql_str)
                conn.commit()

            hours = data["hours"]
            for day in hours:
                times = hours[day].split('-')
                sql_str = "INSERT INTO Hours(business_id, open_time, close_time, day) " + " VALUES ('" + data['business_id'] + "','" + times[0] + "','" + times[1] + "','" + day + "');"
                try:
                    cur.execute(sql_str)
                except:
                    print("Insert to hoursTable failed!")
                    print(sql_str)
                conn.commit()


            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2UserTable():
    #reading the JSON file

    with open('yelp_user.JSON','r') as f:    #TODO: update path for the input file

        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='test' user='postgres' host='localhost' port='8212' password='123'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            if(count_line%1000 == 0):
                print("Running user table")
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO users (user_id, name, yelping_since, tip_count, fans_count, average_stars, funny_count, useful_count, cool_count) " \
                      "VALUES ('" + cleanStr4SQL(data['user_id']) + "','" + cleanStr4SQL(data['name']) + "','" + \
                          data['yelping_since'] + "'," + str(data['tipcount']) + "," + str(data['fans']) + \
                              "," + str(data['average_stars']) + "," + str(data['funny']) + "," + str(data['useful']) + "," + \
                                 str(data['cool']) + ");"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to userTABLE failed!")
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            for friend_id in data['friends']:
                sql_str = "INSERT INTO Friendswith(user_id, friend_id) " + " VALUES ('" + data['user_id'] + "','" + friend_id + "');"
                try:
                    cur.execute(sql_str)
                except:
                    print("Insert to Friendswith failed!")
                    print(sql_str)
                conn.commit()

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2ChecksinTable():
    #reading the JSON file
    with open('yelp_checkin.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='test' user='postgres' host='localhost' port='8212' password='123'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            if(count_line%1000 == 0):
                print("Running checkins table")
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            cis = data['date'].split(",")
            for ci in cis:
                sql_str = "INSERT INTO checksin (business_id, checkin_time) " \
                          "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + ci +  "');"
                try:
                    cur.execute(sql_str)
                except:
                    print("Insert to checksinTABLE failed!")
                    print(sql_str)
                conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)
            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2TipTable():
    #reading the JSON file
    with open('yelp_tip.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='test' user='postgres' host='localhost' port='8212' password='123'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            if(count_line%1000 == 0):
                print("Running tip table")
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO tips (business_id, user_id, date_created, like_count, tip_text) " \
                      "VALUES ('" + cleanStr4SQL(data['business_id']) + \
                          "','" + cleanStr4SQL(data['user_id']) + "','" + cleanStr4SQL(data['date']) + \
                              "'," + str(data['likes']) +",'" + cleanStr4SQL(data['text']).replace("'","''") + "');"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to tipTABLE failed!")
                print(sql_str)
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)
            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

insert2BusinessTable()
print("Done 1")
insert2UserTable()
print("Done 2")
insert2TipTable()
print("Done 3")
insert2ChecksinTable()
print("Done 4")
print("Done")
exit()