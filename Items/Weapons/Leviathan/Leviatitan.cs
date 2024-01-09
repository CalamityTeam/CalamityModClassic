using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Leviathan {
public class Leviatitan : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Leviatitan");
	}

    public override void SetDefaults()
    {
        Item.damage = 103;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 82;
        Item.height = 28;
        Item.useTime = 9;
        Item.useAnimation = 9;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5f;
        Item.value = 750000;
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item92;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("AquaBlast").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 18f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "IOU");
        recipe.AddIngredient(null, "LivingShard");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
        float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
        float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
        if (Main.rand.NextBool(3))
        {
        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("AquaBlastToxic").Type, (int)((double)damage * 1.5f), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        else
        {
        	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("AquaBlast").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        }
    	return false;
	}
}}