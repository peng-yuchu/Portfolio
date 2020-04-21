import json

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")


def parseBusinessData():
    #read the JSON file
    with open('yelp_business.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('business.txt', 'w')
        line = f.readline()
        count_line = 0
        i = 1
        #read each JSON abject and extract data
        outfile.write("HEADER: (business_id, name; address; state; city; postal_code; latitude; longitude; stars; is_open)\n")
        while line:
            data = json.loads(line)
            outfile.write(str(i)+'-\t'+"business info:")
            outfile.write('\''+cleanStr4SQL(data['business_id'])+'\' ; ') #business id
            outfile.write('\''+json.dumps(data['name'])+'\' ; ') #name
            outfile.write('\''+json.dumps(data['address'])+'\' ; ') #full_address
            outfile.write('\''+cleanStr4SQL(data['state'])+'\' ; ') #state
            outfile.write('\''+cleanStr4SQL(data['city'])+'\' ; ') #city
            outfile.write('\''+cleanStr4SQL(data['postal_code'])+'\' ; ')  #zipcode
            outfile.write(str(data['latitude'])+' ; ') #latitude
            outfile.write(str(data['longitude'])+' ; ') #longitude
            outfile.write(str(data['stars'])+' ; ') #stars
            outfile.write(str(data['review_count'])+' ; ') #reviewcount
            outfile.write(str(data['is_open'])+' ;\n') #openstatus

            categories = data["categories"].split(', ')
            outfile.write('\t'+'categories:'+ str(categories)+'\n')  #category list

            outfile.write('\t'+'attributes:'+ json.dumps(data['attributes'])+'\n') # write your own code to process attributes
            outfile.write('\t'+'hours:'+str(data['hours'])) # write your own code to process hours
            outfile.write('\n');

            line = f.readline()
            count_line +=1
            i +=1
    print(count_line)
    outfile.close()
    f.close()

def parseUserData():
    #write code to parse yelp_user.JSON
    with open('yelp_user.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('user.txt', 'w')
        line = f.readline()
        count_line = 0
        i = 0
        #read each JSON abject and extract data
        outfile.write("HEADER: (user_id, name; yelping_since; tipcount; fans; average_stars; (funny,useful,cool))\n")
        while line:
            data = json.loads(line)
            outfile.write(str(i)+'-\t'+"user info:")
            outfile.write('\''+cleanStr4SQL(data['user_id'])+'\' ; ') #user id
            outfile.write('\''+json.dumps(data['name'])+'\' ; ') #name
            outfile.write('\''+cleanStr4SQL(data['yelping_since'])+'\' ; ') #yelping since
            outfile.write('\''+str(data['tipcount'])+'\' ; ') #tipcount
            outfile.write('\''+str(data['fans'])+'\' ; ') #fans
            outfile.write('\''+str(data['average_stars'])+'\' ; ')  #average stars
            outfile.write('('+str(data['funny'])+',') #funny
            outfile.write(str(data['useful'])+',') #useful
            outfile.write(str(data['cool'])+')\n') #cool
            outfile.write('\t'+'friends:'+ str((data['friends']))+'\n')  #friend list

            line = f.readline()
            count_line +=1
            i +=1
    print(count_line)
    outfile.close()
    f.close()

def parseCheckinData():
    #write code to parse yelp_checkin.JSON
    with open('yelp_checkin.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('checkin.txt', 'w')
        line = f.readline()
        count_line = 0
        i = 1
        #read each JSON abject and extract data
        outfile.write("HEADER: (business_id: (year month,day,time))\n")
        while line:
            data = json.loads(line)
            outfile.write(str(i)+'-  \''+cleanStr4SQL(data['business_id'])+'\':\n') #business id

            date = str((str(data["date"]).replace(" ","\' \'").split(',')))
            date = date.replace("[","").replace("\"","(\'").replace("\' \'","\',\'").replace("(\',","\' ) ").replace("-","\',\'")
            outfile.write(str(date)+'\t') #name
            outfile.write('\n')
            line = f.readline()
            count_line +=1
            i +=1
    print(count_line)
    outfile.close()
    f.close()


def parseTipData():
    #write code to parse yelp_tip.JSON
    with open('yelp_tip.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('tip.txt', 'w')
        line = f.readline()
        count_line = 0
        i = 1
        #read each JSON abject and extract data
        outfile.write("HEADER: (business_id; date; likes; text; user_id)\n")
        while line:
            data = json.loads(line)
            outfile.write(str(i)+'-  ')
            outfile.write('\''+cleanStr4SQL(data['business_id'])+'\' ; ') #business id
            outfile.write(str(data['date'])+'\' ; ') #date
            outfile.write(str(data['likes'])+' ; ') #like
            outfile.write(json.dumps(data['text']).replace("\"","\'")+' ; ') #text
            outfile.write(json.dumps(data['user_id']).replace("\"","\'")+'\n') #user_id


            line = f.readline()
            count_line +=1
            i +=1
    print(count_line)
    outfile.close()
    f.close()

parseBusinessData()
parseUserData()
parseCheckinData()
parseTipData()
