FROM djfarrelly/maildev

CMD ["bin/maildev", "--web", "80", "--smtp", "25", "--outgoing-host", "172.20.0.133", "--outgoing-port", "25"]