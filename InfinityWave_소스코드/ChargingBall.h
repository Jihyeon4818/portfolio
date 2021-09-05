// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "ArenaBattle.h"
#include "GameFramework/Actor.h"
#include "ChargingBall.generated.h"


UCLASS()
class ARENABATTLE_API AChargingBall : public AActor
{
	GENERATED_BODY()

public:
	// Sets default values for this actor's properties
	AChargingBall();

	virtual void PostInitializeComponents() override;

	UPROPERTY(VisibleAnywhere, BlueprintReadOnly, Category = Movement, meta = (AllowPrivateAccess = "true"))
	class UProjectileMovementComponent* ChargingBallMovement;

	UPROPERTY(VisibleAnywhere, Category = Effect)
		UParticleSystemComponent* Effect;


};
