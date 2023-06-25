FROM gcr.io/buildpacks/builder
USER root
RUN apt-get update && apt-get install -y --no-install-recommends \
  subversion && \
  apt-get clean && \
  rm -rf /var/lib/apt/lists/*
USER cnb