﻿using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using TekConf.RemoteData.Dtos.v1;
using TekConf.RemoteData.v1;

namespace TekConf.UI.Web.Controllers
{
  public class SessionController : AsyncController
  {
    public string BaseUrl()
    {
      return ConfigurationManager.AppSettings["baseUrl"];
    }

    public void IndexAsync(string conferenceSlug)
    {
      var remoteData = new RemoteDataRepository(BaseUrl());
      AsyncManager.OutstandingOperations.Increment();
      remoteData.GetSessions(conferenceSlug, sessions =>
      {
        AsyncManager.Parameters["sessions"] = sessions;
        AsyncManager.OutstandingOperations.Decrement();
      });

    }
    
    public ActionResult IndexCompleted(List<SessionsDto> sessions)
    {
      return View(sessions);
    }

    public void DetailAsync(string conferenceSlug, string sessionSlug)
    {
      var remoteData = new RemoteDataRepository(BaseUrl());
      AsyncManager.OutstandingOperations.Increment();
      remoteData.GetSession(conferenceSlug, sessionSlug, session =>
      {
        AsyncManager.Parameters["session"] = session;
        AsyncManager.OutstandingOperations.Decrement();
      });
    }

    public ActionResult DetailCompleted(SessionDto session)
    {
      return View(session);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public void CreateAsync(SessionDto session)
    {
      var remoteData = new RemoteDataRepository(BaseUrl());
      AsyncManager.OutstandingOperations.Increment();
      //remoteData.AddSession(session, b =>
      //{
        //AsyncManager.Parameters["conference"] = conference;
        AsyncManager.OutstandingOperations.Decrement();
      //});
    }

    public ActionResult CreateCompleted()
    {
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      return View();
    }

    [HttpPost]
    public ActionResult Edit(int id, FormCollection collection)
    {
      try
      {
        // TODO: Add update logic here

        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }

    public ActionResult Delete(int id)
    {
      return View();
    }

    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection)
    {
      try
      {
        // TODO: Add delete logic here

        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }
  }
}
