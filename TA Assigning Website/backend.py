from __future__ import print_function
import sys
from flask import Flask, jsonify, request
from flask_cors import CORS
import flask_sqlalchemy as sqlalchemy
from sqlalchemy import func

import datetime

app = Flask(__name__)
CORS(app)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///Database.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False

db = sqlalchemy.SQLAlchemy(app)
base_url = '/api/'
class AppliedCourses(db.Model):
	id = db.Column(db.Integer, primary_key=True)
	appname = db.Column(db.String(150))
	applastname = db.Column(db.String(150))
	appcourse = db.Column(db.String(150))
	applabsection = db.Column(db.String(150))
	appwsuid = db.Column(db.String(150))
	appphone = db.Column(db.String(150))
	appstatus = db.Column(db.String(150),default="Pending")
	appprofname = db.Column(db.String(150))
	appmajor = db.Column(db.String(150),default="Computer Science")
	appgpa = db.Column(db.Float,default=4.0)
	appmonth = db.Column(db.Integer,default=5)
	appyear = db.Column(db.Integer,default=2020)
	appserved = db.Column(db.String(120),default="Yes")
	appemail = db.Column(db.String(128))

	def __init__(self,name,lastname,course,labsection,wsuid,phone,profname,major,gpa,month,year,served,email):
		self.appname = name
		self.applastname = lastname
		self.appcourse = course
		self.applabsection = labsection
		self.appwsuid = wsuid
		self.appphone = phone
		self.appprofname = profname
		self.appmajor = major
		self.appgpa = gpa
		self.appyear = year
		self.appmonth = month
		self.appserved = served
		self.appemail = email


@app.route(base_url + 'apply',methods = ['POST'])
def applyCourse():

	data = request.get_json(force=True)
	newuser = AppliedCourses(data["name"],data["lastname"],data["course"],data["labsection"],data["wsuid"],data["phone"],data["profname"],data["major"],data["gpa"],data["month"],data["year"],data["served"],data["email"])
	db.session.add(newuser)
	db.session.commit()
	db.session.refresh(newuser)

	return jsonify({"status":1,"applied" : row_to_obj_appliedcourses(newuser)}),200

@app.route(base_url + 'remove_applied', methods=['DELETE'])
def delete_appliedcourses():
	course_name = request.args.get('coursename', None)
	lab = request.args.get('appliedlabsection', None)

	if course_name is None:
		return {"status": -1, "error": "must provide course_name"}, 500
	AppliedCourses.query.filter_by(appcourse = course_name,applabsection = lab).delete()
	db.session.commit()
	return jsonify({"status": 1}), 200


class User(db.Model):
	id = db.Column(db.Integer, primary_key=True)
	username = db.Column(db.String(150))
	password = db.Column(db.String(150))
	name = db.Column(db.String(150))
	lastname = db.Column(db.String(150))
	title = db.Column(db.String(150))
	wsuid = db.Column(db.String(150))
	phone = db.Column(db.String(150))
	major = db.Column(db.String(150),default="Computer Science")
	gpa = db.Column(db.Float,default=4.0)
	month = db.Column(db.Integer,default=5)
	year = db.Column(db.Integer,default=2020)
	served = db.Column(db.String(120),default="Yes")

	def __init__(self,username,password,name,lastname,title,wsuid,phone):
		self.username = username
		self.password = password
		self.name = name
		self.lastname = lastname
		self.title = title
		self.wsuid = wsuid
		self.phone = phone



@app.route(base_url + 'login', methods=['GET'])
def login():

	user_name = request.args.get('user_name',None)
	order = user_name

	if order is not None:
		query = User.query.filter_by(username = user_name)
	else:
		query = User.query.all()

	result = []
	for row in query:
		result.append(
			row_to_obj_users(row)
		)
	return jsonify({"status" : 1, "users": result}),200

@app.route(base_url + 'addinfo',methods=['POST'])
def additionalInfo():

    data = request.get_json(force=True)
    add_username = data["email"]

    update_user = User.query.filter_by(username = add_username).first()

    update_user.major = data["major"]
    update_user.gpa = data["gpa"]
    update_user.month = data["grad_m"]
    update_user.year = data["grad_y"]
    update_user.served = data["serve"]

    db.session.commit()

    return jsonify({"status":1}),200

@app.route(base_url + 'assign',methods=['POST'])
def assignTA():

    data = request.get_json(force=True)
    user_email = data["email"]

    update_user = AppliedCourses.query.filter_by(appemail = user_email).first()

    update_user.appstatus = data["status"]

    db.session.commit()

    return jsonify({"status":1}),200


@app.route(base_url + 'newuser',methods=['POST'])
def createAccount():

	data = request.get_json(force=True)
	newuser = User(data["username"],data["password"],data["name"],data["lastname"],data["title"],data["wsuid"],data["phone"])
	db.session.add(newuser)
	db.session.commit()
	db.session.refresh(newuser)

	return jsonify({"status":1,"user" : row_to_obj_users(newuser)}),200



def row_to_obj_users(row):
	myrow = {
			"id": row.id,
			"username": row.username,
			"password": row.password,
			"title": row.title,
			"name":row.name,
			"lastname":row.lastname,
			"wsuid":row.wsuid,
			"phone":row.phone,
			"major":row.major,
			"gpa":row.gpa,
			"month":row.month,
			"year":row.year,
			"served":row.served

		}
	return myrow

class Course(db.Model):
	id = db.Column(db.Integer, primary_key=True)
	name = db.Column(db.String(150))
	professor = db.Column(db.String(150))
	prof_id = db.Column(db.Integer)
	prof_email = db.Column(db.String(250))
	status = db.Column(db.String(150),default="Pending")
	student_name = db.Column(db.String(250),default="Hien Duong")
	student_id = db.Column(db.Integer,default=12345678)
	student_email = db.Column(db.String(250),default="hien.duong@wsu.edu")
	phone_number = db.Column(db.String(256),default = 0)
	lab_section = db.Column(db.String(250))
	posted_at = db.Column(db.DateTime,default= datetime.datetime.utcnow)

	def __init__(self,name,professor,prof_id,lab_section,prof_email):
		self.name = name
		self.professor = professor
		self.prof_id = prof_id
		self.lab_section = lab_section
		self.prof_email = prof_email

base_url = '/api/'

@app.route(base_url + 'applicants', methods=['GET'])
def display_applicants():

	course_name = request.args.get('course_name',None)

	professor_name = request.args.get('professor_name',None)

	applicant_id = request.args.get('applicant_id',None)

	if course_name is not None:
		query = AppliedCourses.query.filter_by(appcourse = course_name,appprofname = professor_name)
	elif professor_name is not None:
		query = AppliedCourses.query.filter_by(appprofname = professor_name)
	elif applicant_id is not None:
		query = AppliedCourses.query.filter_by(appwsuid = applicant_id)
	else:
		query = AppliedCourses.query.all()

	result = []

	for row in query:
		result.append(
			row_to_obj_appliedcourses(row)
		)
	return jsonify({"status" : 1, "applied" : result}),200

@app.route(base_url + 'courses', methods=['GET'])
def display_course():

	course_name = request.args.get('course_name',None)

	professor_name = request.args.get('professor_name',None)

	semail = request.args.get('student_email',None)

	if course_name is not None:
		query = Course.query.filter_by(name = course_name,prof_email = professor_name)
	elif professor_name is not None:
		query = Course.query.filter_by(prof_email = professor_name).order_by(Course.name)
	elif semail is not None:
		query = Course.query.filter_by(student_email = semail)
	else:
		query = Course.query.all()

	result = []

	for row in query:
		result.append(
			row_to_obj_courses(row)
		)
	return jsonify({"status" : 1, "courses" : result}),200

@app.route(base_url + 'new_course',methods = ['POST'])
def create_course():

	data = request.get_json(force=True)

	newcourse = Course(data["name"],data["professor"],data["prof_id"],data["lab_section"],data["prof_email"])

	db.session.add(newcourse)
	db.session.commit()
	db.session.refresh(newcourse)

	return jsonify({"status":1, "course" : row_to_obj_courses(newcourse)}),200

@app.route(base_url + 'remove', methods=['DELETE'])
def delete_users():
	user_name = request.args.get('username', None)

	if user_name is None:
		return {"status": -1, "error": "must provide id"}, 500
	User.query.filter_by(username = user_name).delete()
	db.session.commit()
	return jsonify({"status": 1}), 200

@app.route(base_url + 'remove_course', methods=['DELETE'])
def delete_courses():
	course_name = request.args.get('coursename', None)
	labsect = request.args.get('labsection', None)

	if course_name is None:
		return {"status": -1, "error": "must provide course_name"}, 500
	Course.query.filter_by(name = course_name,lab_section = labsect).delete()
	db.session.commit()
	return jsonify({"status": 1}), 200


def row_to_obj_courses(row):
	myrow = {
			"id": row.id,
			"name":row.name,
			"professor":row.professor,
			"prof_id":row.prof_id,
			"status":row.status,
			"student_name":row.student_name,
			"student_id":row.student_id,
			"student_email":row.student_email,
			"phone_number":row.phone_number,
			"lab_section":row.lab_section,
			"prof_email":row.prof_email,
			"posted_at":row.posted_at
		}
	return myrow

def row_to_obj_appliedcourses(row):
	myrow = {
			"id": row.id,
			"name":row.appname,
			"lastname":row.applastname,
			"course":row.appcourse,
			"labsection":row.applabsection,
			"wsuid":row.appwsuid,
			"phone":row.appphone,
			"status":row.appstatus,
			"profname":row.appprofname,
			"major":row.appmajor,
			"gpa":row.appgpa,
			"month":row.appmonth,
			"year":row.appyear,
			"served":row.appserved,
			"email":row.appemail
		}
	return myrow

def main():
	db.create_all()
	app.run()

if __name__ == '__main__':
	app.debug = True
	main()
