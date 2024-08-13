from flask import Blueprint

bp = Blueprint('visio', __name__)


from clipschuss_server.blueprints.api import routes
