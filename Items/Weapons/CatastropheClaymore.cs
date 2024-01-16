using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class CatastropheClaymore : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/CatastropheClaymore");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Catastrophe Claymore");
		Item.width = 48;  //The width of the .png file in pixels divided by 2.
		Item.damage = 68;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 23;
		Item.useTime = 23;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 6.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 48;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		////Tooltip.SetDefault("Remnant of a specular nova");
		Item.value = 750000;  //Value is calculated in copper coins.
		Item.rare = 7;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("CalamityAura").Type;
		Item.shootSpeed = 11f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	switch (Main.rand.Next(3))
		{
    		case 0: type = Mod.Find<ModProjectile>("CalamityAura").Type; break;
    		case 1: type = Mod.Find<ModProjectile>("CalamityAuraType2").Type; break;
    		case 2: type = Mod.Find<ModProjectile>("CalamityAuraType3").Type; break;
    		default: break;
		}
       	Projectile.NewProjectile(source, position, velocity,type, damage, knockback, Main.myPlayer);
    	return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.HallowedBar, 10);
		recipe.AddIngredient(ItemID.CrystalShard, 15);
		recipe.AddIngredient(ItemID.SoulofNight, 5);
		recipe.AddIngredient(ItemID.CursedFlame, 5);
		recipe.AddIngredient(ItemID.SoulofMight, 3);
		recipe.AddIngredient(ItemID.SoulofSight, 3);
		recipe.AddIngredient(ItemID.SoulofFright, 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.HallowedBar, 10);
		recipe.AddIngredient(ItemID.CrystalShard, 15);
		recipe.AddIngredient(ItemID.SoulofNight, 5);
		recipe.AddIngredient(ItemID.Ichor, 5);
		recipe.AddIngredient(ItemID.SoulofMight, 3);
		recipe.AddIngredient(ItemID.SoulofSight, 3);
		recipe.AddIngredient(ItemID.SoulofFright, 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 73);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if(Main.rand.Next(3) == 0)
    	{
    		target.AddBuff(BuffID.CursedInferno, 200);
    		target.AddBuff(BuffID.OnFire, 200);
    		target.AddBuff(BuffID.Frostburn, 200);
    	}
	}
}}
