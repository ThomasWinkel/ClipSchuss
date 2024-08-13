from flask import Blueprint

bp = Blueprint('visio', __name__)


from app.blueprints.api import routes
