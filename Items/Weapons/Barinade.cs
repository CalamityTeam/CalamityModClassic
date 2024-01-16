using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Barinade : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/1.DesertScourge/Barinade");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Barinade");
        Item.damage = 12;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 30;
        Item.height = 44;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Shoots electric bolt arrows that explode");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2f;
        Item.value = 35000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 18f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("BoltArrow").Type, (int)((double)damage * 0.5f), knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}