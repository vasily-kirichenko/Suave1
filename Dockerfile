FROM mono
COPY ./Suave1/bin/Release/ /app
EXPOSE 8083
WORKDIR /app
CMD ["mono", "Suave1.exe"]