using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class NuclearFury : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture = "CalamityModClassic1Point0/Items/Weapons/NuclearFury");
		return true;
	}
	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Nuclear Fury");
        Item.damage = 65;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 13;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        //Tooltip.SetDefault("Casts a torrent of cosmic typhoons");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5;
        Item.value = 2500000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item84;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("NuclearFuryProjectile").Type;
        Item.shootSpeed = 16f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
            
    	float spread = 45f * 0.0174f;
    	double startAngle = Math.Atan2(velocity.X, velocity.Y)- spread/2;
    	double deltaAngle = spread/8f;
    	double offsetAngle;
    	int i;
    	for (i = 0; i < 4; i++ )
    	{
   		offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
        	Terraria.Projectile.NewProjectile(source, position.X, position.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
        	Terraria.Projectile.NewProjectile(source, position.X, position.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
    	}
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 6);
        recipe.AddIngredient(ItemID.SoulofSight, 15);
        recipe.AddIngredient(ItemID.UnicornHorn, 5);
        recipe.AddIngredient(ItemID.RazorbladeTyphoon);
        recipe.AddTile(TileID.CrystalBall);
        recipe.Register();
    }
}}